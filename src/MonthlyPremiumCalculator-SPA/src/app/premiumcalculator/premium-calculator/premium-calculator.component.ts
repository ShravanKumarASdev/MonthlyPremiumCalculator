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

  onDateChange(){
    //Code to find age on Date change
    let timeDiff = Math.abs(Date.now() - new Date(this.formData.DateOfBirth).getTime());
    this.formData.Age = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
  }

  //Triggers Premium calculation and fields validation on Occupation change and Submit click
  calculatePremium(){
    this.submitted = true;

    //Will not call Service method to calculate premium unless form is valid
    if(this.form.invalid){
      return;
    }
    this.premiumCalculatorService.calculateMonthlyPremium(this.formData)
    .subscribe((data)=>{this.calculatedPremium = data; this.openPopup();});
  }

  //Retrieves Form controls
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
  
  //Method to open Premium display popup
  openPopup() {
    this.displayStyle = "block";
  }

  //Method to close Premium display popup
  closePopup() {
    this.displayStyle = "none";
  }

  //Method to reset the Premium Calculator UI
  reset(){
    this.submitted = false;
    this.form.reset();
  }

  ngOnInit(): void {
    //Initializing Form Validators
    this.form = this.formBuilder.group(
      {
        name: ['',[Validators.required, Validators.pattern('^[a-zA-Z ]+$')]],//Required and should allow only letters
        age: [],
        dateOfBirth: ['',Validators.required],
        occupation: ['',
            Validators.required
          ],
        deathSumInsured: ['',[Validators.required, Validators.pattern(/^[1-9]+[0-9]*$/)]]//Required and should allow only numbers greater than zero
      }
    );

    this.occupationsService.retrieveOccupations().subscribe(result => this.occupations = result);
  }

}
