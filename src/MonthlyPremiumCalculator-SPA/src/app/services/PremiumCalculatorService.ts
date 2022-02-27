import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PremiumInputModel } from '../models/PremiumModel';

@Injectable({
    providedIn: 'root'
})
export class PremiumCalculatorService {
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor(private http: HttpClient) { }    

    public calculateMonthlyPremium(premiumInputModel: PremiumInputModel) {
        return this.http.post(this.baseUrl + 'premiumcalculator/', premiumInputModel);
    }
}