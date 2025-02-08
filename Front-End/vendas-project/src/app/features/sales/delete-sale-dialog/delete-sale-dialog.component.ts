import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-sale-dialog',
  templateUrl: './delete-sale-dialog.component.html',
  styleUrls: ['./delete-sale-dialog.component.scss'],
})
export class DeleteSaleDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteSaleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
