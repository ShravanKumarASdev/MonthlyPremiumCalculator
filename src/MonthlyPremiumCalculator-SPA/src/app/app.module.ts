import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {PremiumCalculatorService} from './services/PremiumCalculatorService';
import { PremiumCalculatorComponent } from './premiumcalculator/premium-calculator/premium-calculator.component';

@NgModule({
  declarations: [
    AppComponent,
    PremiumCalculatorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [PremiumCalculatorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
