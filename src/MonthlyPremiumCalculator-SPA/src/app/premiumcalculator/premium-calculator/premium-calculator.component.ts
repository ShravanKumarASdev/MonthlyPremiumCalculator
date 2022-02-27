import { Component, OnInit } from '@angular/core';
import { PremiumInputModel } from 'src/app/models/PremiumModel';

@Component({
  selector: 'app-premium-calculator',
  templateUrl: './premium-calculator.component.html',
  styleUrls: ['./premium-calculator.component.css']
})
export class PremiumCalculatorComponent implements OnInit {
  public formData: PremiumInputModel;

  constructor() { }

  ngOnInit(): void {
  }

}
