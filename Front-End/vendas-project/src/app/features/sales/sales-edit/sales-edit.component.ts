import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { SalesService } from 'src/app/core/services/sales.service';

@Component({
  selector: 'app-sales-edit',
  templateUrl: './sales-edit.component.html',
  styleUrls: ['./sales-edit.component.scss'],
})
export class SalesEditComponent implements OnInit {
  displayedColumns: string[] = ['product', 'quantity', 'unitPrice', 'actions'];

  constructor(
    public dialogRef: MatDialogRef<SalesEditComponent>,
    private salesService: SalesService,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {}

  onSave(): void {
    this.dialogRef.close(this.data);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onEdit(): void {
    if (!this.data || !this.data.id) {
      return;
    }

    this.salesService.updateSale(this.data.id, this.data).subscribe({
      next: (response) => {
        this.toastr.success('Venda atualizada com sucesso!');
      },
      error: (err) => {
        this.toastr.error('Erro ao atualizar a venda');
      },
    });
  }

  onDelete(): void {
    console.log('Delete Sale');
  }

  onDeleteItem(item: any): void {
    console.log('Excluindo item:', item);
  }
  onToggleStatus(item: any): void {
    const isNowCancelled = !item.isCancelled;
    item.isCancelled = isNowCancelled;

    this.salesService.cancelSaleItem(item.id, isNowCancelled).subscribe({
      next: () => {
        const message = isNowCancelled
          ? 'Item de venda inativado com sucesso!'
          : 'Item de venda ativado com sucesso!';
        this.toastr.success(message);
      },
      error: () => {
        const errorMessage = isNowCancelled
          ? 'Erro ao inativar o item da venda'
          : 'Erro ao ativar o item da venda';
        this.toastr.error(errorMessage);
      },
    });
  }
}
