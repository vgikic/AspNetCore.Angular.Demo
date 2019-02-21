import { Observable } from 'rxjs';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AuthenticationService } from '../authentication.service';



@Injectable({ providedIn: 'root' })
export class AuthenticationInterceptorService implements HttpInterceptor {

    constructor(private router: Router, private authService: AuthenticationService) { }

    private handleError = (error: any): Observable<HttpEvent<any> | HttpResponse<boolean>> => {
        if (error.status === 401) {
            alert("401")
        } else if (error.status === 403) {
            this.router.navigate(['forbidden']);
        } else if (error.status === 404) {
            alert("404")
        }
        return throwError(error);
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const requestClone = req.clone({
            setHeaders: {
                'Content-Type': 'application/json',
                'Cache-Control': 'no-cache',
                'Pragma': 'no-cache',
                'Authorization': this.authService.userAuth ? `Bearer ${this.authService.userAuth.bearerToken}` : ''
            },
            withCredentials: true,
        });
        return next.handle(requestClone)
            .pipe(
                tap
                    (event => {
                        if (event.type === HttpEventType.Response) {
                            // console.log("Request response: ", event.body);
                        }
                    }),
                catchError((err: HttpErrorResponse, caught) => this.handleError(err)))
    }
}
