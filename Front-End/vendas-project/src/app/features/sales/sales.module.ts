import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SalesRoutingModule } from './sales-routing.module';
import { SalesListComponent } from './sales-list/sales-list.component';
import { SalesEditComponent } from './sales-edit/sales-edit.component';
import { MaterialModule } from 'src/app/shared/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteSaleDialogComponent } from './delete-sale-dialog/delete-sale-dialog.component';

@NgModule({
  declarations: [
    SalesListComponent,
    SalesEditComponent,
    DeleteSaleDialogComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    SalesRoutingModule,
    FormsModule,
  ],
  exports: [MaterialModule],
})
export class SalesModule {}
