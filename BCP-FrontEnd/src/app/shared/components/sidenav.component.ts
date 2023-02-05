import { BreakpointObserver } from '@angular/cdk/layout'
import { Component, ViewChild } from '@angular/core'
import { MatSidenav } from '@angular/material/sidenav'

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent {
  @ViewChild(MatSidenav)
    sideNav: MatSidenav

  constructor (
    private readonly observer: BreakpointObserver
  ) {}

  ngAfterViewInit (): void {
    this.observer.observe(['(max-width:800px)']).subscribe(res => {
      if (res.matches) {
        this.sideNav.mode = 'over'
        void this.sideNav.close()
      } else {
        this.sideNav.mode = 'side'
        void this.sideNav.open()
      }
    })
  }
}
