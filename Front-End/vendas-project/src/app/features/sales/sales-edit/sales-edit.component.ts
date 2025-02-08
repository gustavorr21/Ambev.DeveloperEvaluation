import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
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
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {}

  onSave(): void {
    // Lógica para salvar os dados alterados
    this.dialogRef.close(this.data);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onEdit(): void {
    if (!this.data || !this.data.id) {
      console.error('Erro: ID da venda não encontrado.');
      return;
    }

    this.salesService.updateSale(this.data.id, this.data).subscribe({
      next: (response) => {
        console.log('Venda atualizada com sucesso!', response);
      },
      error: (err) => {
        console.error('Erro ao atualizar a venda', err);
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
    item.isActive = !item.isActive;
    console.log('Novo status do item:', item.isActive ? 'Ativo' : 'Inativo');
  }
}
