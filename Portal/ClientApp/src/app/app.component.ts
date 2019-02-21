import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationCancel } from '@angular/router';
import { SpinnerInterceptorService } from './services/interceptors/spinner-interceptor.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.subscribeToRouteEvents();
  }

  constructor(
    private router: Router,
    private spinnerService: SpinnerInterceptorService
  ) { }

  ngOnInit(): void {
    this.subscribeToRouteEvents();
  }

  private subscribeToRouteEvents = () => {
    const element = document.getElementById('script-load');
    this.router.events.subscribe((e) => {
      if (e instanceof NavigationStart) {
        element.classList.add('visible');
        // this.spinnerService.showSpinner();
      } else if (e instanceof NavigationEnd || e instanceof NavigationCancel) {
        element.classList.remove('visible');
      }
    })
  }

}
