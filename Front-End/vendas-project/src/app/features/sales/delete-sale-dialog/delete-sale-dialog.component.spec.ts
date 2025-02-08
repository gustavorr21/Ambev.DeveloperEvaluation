import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteSaleDialogComponent } from './delete-sale-dialog.component';

describe('DeleteSaleDialogComponent', () => {
  let component: DeleteSaleDialogComponent;
  let fixture: ComponentFixture<DeleteSaleDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteSaleDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteSaleDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
