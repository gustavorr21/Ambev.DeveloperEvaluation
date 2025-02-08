import { DecimalPipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { SalesService } from 'src/app/core/services/sales.service';

@Component({
  selector: 'app-sales-edit',
  templateUrl: './sales-edit.component.html',
  styleUrls: ['./sales-edit.component.scss'],
  providers: [DecimalPipe],
})
export class SalesEditComponent implements OnInit {
  editSaleForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private decimalPipe: DecimalPipe,
    private salesService: SalesService,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<SalesEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.editSaleForm = this.fb.group({
      client: [this.data?.client || ''],
      branch: [this.data?.branch || ''],
      isCancelled: [this.data?.isCancelled || ''],
      saleNumber: [this.data?.saleNumber || ''],
      totalValue: [{ value: this.data?.totalValue || 0, disabled: true }],
      items: this.fb.array(
        this.data?.items?.map((item: any) => this.createItem(item)) || []
      ),
    });
    this.calculateTotal();
  }

  get items(): FormArray {
    return this.editSaleForm.get('items') as FormArray;
  }

  createItem(item?: any): FormGroup {
    return this.fb.group({
      id: [item?.id || null],
      product: [item?.product || '', Validators.required],
      quantity: [
        item?.quantity || 1,
        [Validators.required, Validators.min(1), Validators.max(20)],
      ],
      unitPrice: [
        item?.unitPrice || 0,
        [Validators.required, Validators.min(0.01)],
      ],
      discount: [{ value: item?.discount || 0, disabled: true }],
    });
  }

  addItem(): void {
    this.items.push(this.createItem());
    this.calculateTotal();
  }

  removeItem(index: number): void {
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

      item
        .get('discount')
        ?.setValue(this.decimalPipe.transform(itemDiscount, '1.2-2'));
      total += discountedPrice;
    });

    this.editSaleForm.get('totalValue')?.setValue(total);
  }

  getTotalDiscount() {
    return this.items.controls.reduce(
      (total, item) => total + (item.get('discount')?.value || 0),
      0
    );
  }

  updateSale(): void {
    if (this.editSaleForm.valid) {
      const saleData = this.editSaleForm.getRawValue();

      this.salesService.updateSale(this.data.id, saleData).subscribe(
        () => {
          this.dialogRef.close(true);
          this.toastr.success('Venda criada com sucesso!');
        },
        (error) => {
          this.toastr.error('Erro ao criar venda!');
        }
      );
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
