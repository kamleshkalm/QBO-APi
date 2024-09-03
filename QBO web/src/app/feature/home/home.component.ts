import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { customXmlToJson } from 'src/app/customxml';
import { DetailsService } from 'src/app/detailsService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  accessToken: string = "";
  companyinfo: any;
  dailyupdate: any;
  jsonData: any;
  searchQuery: any;
  invoices: any;
  showPopup: boolean = false;
  customerName: string = '';
  customerId: string = '';
  billEmail: string = '';
  amount: string = '';
  itemRef_value: string = '';
  iscreated: boolean = false;
  product_name: string = '';
  constructor(private dataService: DetailsService, private cookieService: CookieService) { }

  ngOnInit(): void {
    debugger
    const token = this.cookieService.get('access_token');
    if (token) {
      console.log('Access Token:', token);
      this.accessToken = token;
      this.GetDailyUpdates();
      this.Getcompanyinfo();
      // Store the access token or use it for API calls
    } else {
      console.error('Token not found in cookies');
    }
  }

  Getcompanyinfo() {
    this.dataService.Getcompanyinfo(this.accessToken).subscribe((data) => {
      console.log(data);
      this.companyinfo = data;
    })
  }
  GetDailyUpdates() {
    this.dataService.GetdailyUpdates(this.accessToken).subscribe((data) => {
      this.dailyupdate = data;
      console.log("daujmvf", this.dailyupdate);
      this.jsonData = customXmlToJson(this.dailyupdate);
      console.log('Converted JSON:', this.jsonData);
      console.log(this.jsonData.QueryResponse.Invoice[0].Balance);
      this.invoices = this.jsonData.QueryResponse.Invoice;

    })
  }
  downloadInvoice(invoiceId: string): void {
    this.dataService.getInvoicePdf(this.accessToken, invoiceId).subscribe((pdfBlob: Blob) => {
      debugger
      const url = window.URL.createObjectURL(pdfBlob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `invoice_${invoiceId}.pdf`;
      link.click();
      window.URL.revokeObjectURL(url);
    });
  }
  viewInvoice(invoiceId: string): void {
    this.dataService.getInvoicePdf(this.accessToken, invoiceId).subscribe((pdfBlob: Blob) => {
      debugger
      const url = window.URL.createObjectURL(pdfBlob);
      window.open(url);
    });
  }
  CreateInvoice() {
    const payload = {
      invoiceDto: {
        Line: [
          {
            DetailType: 'SalesItemLineDetail',
            Amount: this.amount,
            SalesItemLineDetail: {
              ItemRef: {
                value: this.itemRef_value,
                name: this.product_name
              }
            }
          }
        ],
        CustomerRef: {
          value: '456'
        }
      },
      accessToken: this.accessToken
    };
    const invoice_payload = {
      AccessToken:this.accessToken,
      Amount:this.amount,
      Product_name:this.product_name,
      product_price:this.itemRef_value,
      Customer_ref:'4567'
    }
    debugger
    this.dataService.createInvoice(invoice_payload).subscribe((data: any) => {
      debugger
      if (data) {
        this.iscreated = true;
      }
    });
  }
  openPopup() {
    debugger
    this.showPopup = true;
  }

  closePopup() {
    this.showPopup = false;
  }
}