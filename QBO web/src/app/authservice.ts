import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7207/api/Home'; 

  constructor(private http: HttpClient) { }

  getToken(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/token`);
  }
  authorize(){
    window.location.href = `${this.apiUrl}/authorize`;
  }

  handleCallback(code: string): Observable<any> {
    debugger;
    return this.http.get<any>(`${this.apiUrl}/callback?code=${code}`);
  }
}
