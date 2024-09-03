import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {
  private apiUrl = 'https://localhost:7207/api/Home'; 
  private api2Url = 'https://localhost:7207/api/Invoice'; 

  constructor(private http: HttpClient) { }

  Getcompanyinfo(accessToken: string): Observable<any> {
    debugger;
    const params = new HttpParams().set('accessToken', accessToken);
    return this.http.get<any>(`${this.apiUrl}/companyinfo`, { params });
    // return this.http.get<any>(`${this.apiUrl}/companyinfo`,code);
  }
  GetdailyUpdates(accessToken: string): Observable<any>{
    const params = new HttpParams().set('accessTokenfromcallback', accessToken);
    return this.http.get<any>(`${this.apiUrl}/dailyupdates`, { params ,responseType: 'text' as 'json'});
  }
  getInvoicePdf(accessToken:string,invoiceId: string): Observable<Blob> {    
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`,
      'Accept': 'application/pdf'
    });

    const params = new HttpParams()
      .set('token', accessToken)
      .set('invoiceId', invoiceId);

    return this.http.get<Blob>(`${this.apiUrl}/pdf`, {
      headers: headers,
      params: params,
      responseType: 'blob' as 'json'
    }).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error instanceof Blob) {
          error.error.text().then((text) => {
            console.error('Error details:', text);
          });
        } else {
          console.error('Error details:', error.message);
        }
        return throwError(() => new Error('Failed to fetch PDF'));
      })
    );
  }
  createInvoice(invoiceDto: any){
    debugger
    var b = invoiceDto;
    console.log(b);
    var a = JSON.stringify(invoiceDto)
    console.log(a);
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    // const params = new HttpParams()
    // .set('accessToken', invoiceDto.accessToken)
    // .set('invoiceDto', invoiceDto);
    return this.http.post<any>(`${this.api2Url}/createInvoice`, invoiceDto,{headers});
    // return this.http.post<any>(`${this.api2Url}/createInvoice`, invoice, { headers });
  }
}
  

