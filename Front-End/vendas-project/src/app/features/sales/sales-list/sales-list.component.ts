import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { SalesService } from 'src/app/core/services/sales.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { SalesEditComponent } from '../sales-edit/sales-edit.component';
import { DeleteSaleDialogComponent } from '../delete-sale-dialog/delete-sale-dialog.component';
import { CreateSaleComponent } from '../create-sale/create-sale.component';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.scss'],
})
export class SalesListComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'id',
    'date',
    'total',
    'isCancelled',
    'actions',
  ];
  dataSource = new MatTableDataSource<any>();
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(
    private dialog: MatDialog,
    private salesService: SalesService,
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
    sale.isCancelled = !sale.isCancelled;
    const statusMessage = sale.isCancelled ? 'Ativada' : 'Inativada';

    this.salesService.cancelSale(sale.id, sale.isCancelled).subscribe(() => {
      this.toastr.success(`Venda ${statusMessage} com sucesso`);
      this.loadSales();
    });
  }

  deleteSale(saleId: string): void {
    this.salesService.deleteSale(saleId).subscribe(() => {
      this.toastr.success(`Venda deletada com sucesso`);
      this.loadSales();
    });
  }

  openEditDialog(sale: any): void {
    const dialogRef = this.dialog.open(SalesEditComponent, {
      width: '90%',
      data: sale,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadSales();
      }
    });
  }

  openAddSaleDialog(): void {
    const dialogRef = this.dialog.open(CreateSaleComponent, {
      width: '90%',
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
