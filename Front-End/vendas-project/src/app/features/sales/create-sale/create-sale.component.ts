import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SalesService } from 'src/app/core/services/sales.service';

@Component({
  selector: 'app-create-sale',
  templateUrl: './create-sale.component.html',
  styleUrls: ['./create-sale.component.scss'],
})
export class CreateSaleComponent implements OnInit {
  saleForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateSaleComponent>,
    private salesService: SalesService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {
    this.saleForm = this.fb.group({
      client: ['', Validators.required],
      branch: ['', Validators.required],
      saleDate: ['', Validators.required],
      totalValue: [{ value: 0, disabled: true }],
      items: this.fb.array([]),
    });
    this.addItem();
  }

  get items(): FormArray {
    return this.saleForm.get('items') as FormArray;
  }

  ngOnInit(): void {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  addItem() {
    const newItem = this.fb.group({
      product: ['', Validators.required],
      quantity: [
        1,
        [Validators.required, Validators.min(1), Validators.max(20)],
      ],
      unitPrice: [0, [Validators.required, Validators.min(0.01)]],
      discount: [{ value: 0, disabled: true }],
    });

    this.items.push(newItem);
    this.calculateTotal();
  }

  removeItem(index: number) {
    this.items.removeAt(index);
    this.calculateTotal();
  }

  calculateTotal() {
    let total = 0;

    this.items.controls.forEach((item) => {
      const quantity = item.get('quantity')?.value || 0;
      const unitPrice = item.get('unitPrice')?.value || 0;
      let discount = 0;

      if (quantity >= 10 && quantity <= 20) {
        discount = 0.2;
      } else if (quantity >= 4) {
        discount = 0.1;
      }

      const discountedPrice = unitPrice * quantity * (1 - discount);
      const itemDiscount = unitPrice * quantity * discount;

      item.get('discount')?.setValue(itemDiscount);

      total += discountedPrice;
    });

    this.saleForm.get('totalValue')?.setValue(total);
  }

  getTotalDiscount() {
    return this.items.controls.reduce(
      (total, item) => total + (item.get('discount')?.value || 0),
      0
    );
  }

  createSale(): void {
    if (this.saleForm.valid) {
      this.salesService.createSale(this.saleForm.value).subscribe(
        () => {
          this.dialogRef.close(true);
          alert('Venda criada com sucesso!');
        },
        (error) => {
          alert('Erro ao criar venda: ' + error.message);
        }
      );
    }
  }

  cancel(): void {
    this.saleForm.reset();
    this.dialogRef.close();
  }
}
