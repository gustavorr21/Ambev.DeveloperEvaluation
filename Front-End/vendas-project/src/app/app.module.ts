import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SalesModule } from './features/sales/sales.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

const toastrConfig = {
  preventDuplicates: true,
};

@NgModule({
  declarations: [AppComponent],
  imports: [
    MatInputModule,
    BrowserModule,
    AppRoutingModule,
    SalesModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastrModule.forRoot(toastrConfig),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
