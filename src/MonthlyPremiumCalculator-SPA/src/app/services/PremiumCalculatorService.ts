import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
         const headers= new HttpHeaders()
.set('content-type', 'application/json')
.set('Access-Control-Allow-Origin', '*');
        return this.http.post(this.baseUrl + 'premiumcalculator/', premiumInputModel,{ 'headers': headers });
    }
}