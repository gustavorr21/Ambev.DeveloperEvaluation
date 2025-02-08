import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { SalesService } from 'src/app/core/services/sales.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Subject, takeUntil } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { SalesEditComponent } from '../sales-edit/sales-edit.component';
import { DeleteSaleDialogComponent } from '../delete-sale-dialog/delete-sale-dialog.component';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.scss'],
})
export class SalesListComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'date', 'total', 'isActive', 'actions'];
  dataSource = new MatTableDataSource<any>();
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(
    private dialog: MatDialog,
    private salesService: SalesService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadSales();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  loadSales(): void {
    this.salesService.getSales().subscribe((data: any) => {
      this.dataSource = new MatTableDataSource(data.data.data);
      this.dataSource.paginator = this.paginator;
    });
  }

  toggleStatus(sale: any): void {
    sale.isActive = !sale.isActive;
    const statusMessage = sale.isActive ? 'Ativada' : 'Inativada';

    this.salesService.cancelSale(sale.id, sale.isActive).subscribe(() => {
      this.toastr.success(`Venda ${statusMessage} com sucesso`);
      this.loadSales();
    });
  }

  deleteSale(saleId: string): void {
    this.salesService.deleteSale(saleId).subscribe(() => {
      this.loadSales();
    });
  }

  cancelSaleItem(saleId: string, itemId: string): void {
    this.salesService.cancelSaleItem(saleId, itemId).subscribe(() => {
      this.loadSales();
    });
  }

  openEditDialog(sale: any): void {
    const dialogRef = this.dialog.open(SalesEditComponent, {
      width: '960px',
      data: sale,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadSales();
      }
    });
  }

  openDeleteDialog(saleId: string): void {
    const dialogRef = this.dialog.open(DeleteSaleDialogComponent, {
      width: '400px',
      data: { saleId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.salesService.deleteSale(saleId).subscribe(() => {
          this.loadSales();
        });
      }
    });
  }
}
