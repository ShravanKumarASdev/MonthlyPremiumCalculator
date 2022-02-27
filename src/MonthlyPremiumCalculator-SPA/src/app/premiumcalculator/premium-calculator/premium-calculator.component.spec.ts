import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OccupationsService } from 'src/app/services/OccupationsService';

import { PremiumCalculatorComponent } from './premium-calculator.component';
import { Observable, EMPTY, of } from 'rxjs';
import { PremiumCalculatorService } from 'src/app/services/PremiumCalculatorService';
import { DebugElement } from '@angular/core';

describe('PremiumCalculatorComponent', () => {
  let component: PremiumCalculatorComponent;
  let fixture: ComponentFixture<PremiumCalculatorComponent>;
  var premiumCalculatorService:PremiumCalculatorService;
  let premiumCalculatorServiceSpy: any;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PremiumCalculatorComponent ],
      imports: [ HttpClientTestingModule, FormsModule, ReactiveFormsModule ],
      providers: [ 
        { provide: OccupationsService, useClass: FakeOccupationsService },
        PremiumCalculatorService
       ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumCalculatorComponent);
    component = fixture.componentInstance;

    premiumCalculatorService = fixture.debugElement.injector.get(PremiumCalculatorService);
    premiumCalculatorServiceSpy =spyOn(premiumCalculatorService, 'calculateMonthlyPremium').and.callThrough();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open popup by changing display property', () => {
    component.displayStyle="none";
    component.openPopup();
    expect(component.displayStyle).toBe("block");
  });

  it('should close popup by changing display property', () => {
    component.displayStyle="block";
    component.closePopup();
    expect(component.displayStyle).toBe("none");
  });

  it('should reset form', () => {
    component.submitted=true;
    component.form.setValue({name:'test', age:25, dateOfBirth:'21-01-1989', occupation:'Cleaner', deathSumInsured:100});
    component.reset();
    expect(component.submitted).toBeFalsy();
    expect(component.form.value).toEqual({ name: null, age: null, dateOfBirth: null, occupation: null, deathSumInsured: null });
  });

  it('should calculatePremium', () => {
    component.form.setValue({name:'test', age:25, dateOfBirth:'21-01-1989', occupation:'Cleaner', deathSumInsured:100});
    component.calculatePremium();
    expect(premiumCalculatorServiceSpy).toHaveBeenCalled();
  });

  it('should calculate Age', () => {
    component.formData.DateOfBirth = '11-22-1989'
    component.onDateChange();
    expect(component.formData.Age).toBe(32);
  });  

  it('should render title', () => {
    const fixture = TestBed.createComponent(PremiumCalculatorComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Welcome to Premium calculator');
  });
});

class FakeOccupationsService {
  retrieveOccupations() {
    return of([]);
  }
}