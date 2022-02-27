import { Component, OnInit } from '@angular/core';
import { PremiumInputModel } from 'src/app/models/PremiumModel';
import { PremiumCalculatorService } from 'src/app/services/PremiumCalculatorService';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { OccupationsService } from 'src/app/services/OccupationsService';
@Component({
  selector: 'app-premium-calculator',
  templateUrl: './premium-calculator.component.html',
  styleUrls: ['./premium-calculator.component.css']
})
export class PremiumCalculatorComponent implements OnInit {
  public formData: PremiumInputModel =new PremiumInputModel();
  public calculatedPremium:any;
  currentDate : Date =new Date();

  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    age: new FormControl(''),
    occupation: new FormControl(''),
    deathSumInsured: new FormControl(''),
    dateOfBirth: new FormControl('')
  });
  public submitted = false;

  constructor(private premiumCalculatorService: PremiumCalculatorService, private occupationsService: OccupationsService, private formBuilder: FormBuilder) {}


  public displayStyle = "none";
  public occupations:any =[];

  onDataChange(){
    let timeDiff = Math.abs(Date.now() - new Date(this.formData.DateOfBirth).getTime());
    this.formData.Age = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
  }

  calculatePremium(){
    this.submitted = true;

    if(this.form.invalid){
      console.log(this.form);
      return;
    }
    this.premiumCalculatorService.calculateMonthlyPremium(this.formData)
    .subscribe((data)=>{this.calculatedPremium = data; this.openPopup();});
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
  
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }

  reset(){
    this.submitted = false;
    this.form.reset();
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        name: ['',[Validators.required, Validators.pattern('^[a-zA-Z ]+$')]],
        age: [],
        dateOfBirth: ['',Validators.required],
        occupation: ['',
            Validators.required
          ],
        deathSumInsured: ['',[Validators.required, Validators.pattern(/^[1-9]+[0-9]*$/)]]
      }
    );

    this.occupationsService.retrieveOccupations().subscribe(result => this.occupations = result);
  }

}
