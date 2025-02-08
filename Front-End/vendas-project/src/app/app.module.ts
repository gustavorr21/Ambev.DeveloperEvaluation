import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SalesModule } from './features/sales/sales.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import {
  NgxUiLoaderConfig,
  NgxUiLoaderHttpModule,
  NgxUiLoaderModule,
  NgxUiLoaderRouterModule,
} from 'ngx-ui-loader';
import { LoaderInterceptor } from './services/loader.interceptor';

const loaderConfig: NgxUiLoaderConfig = {
  fgsType: 'three-bounce',
  fgsSize: 60,
  fgsColor: '#007bff',
  overlayColor: 'rgba(255, 255, 255, 0.6)',
  blur: 5,
  pbThickness: 5,
  pbColor: '#007bff',
  hasProgressBar: true,
};
const toastrConfig = {
  preventDuplicates: true,
};

@NgModule({
  declarations: [AppComponent],
  imports: [
    NgxUiLoaderModule.forRoot(loaderConfig),
    NgxUiLoaderRouterModule,
    MatInputModule,
    BrowserModule,
    AppRoutingModule,
    SalesModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastrModule.forRoot(toastrConfig),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
