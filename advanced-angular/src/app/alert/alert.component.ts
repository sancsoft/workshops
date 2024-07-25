import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [],
  templateUrl: './alert.component.html',
  styles: ``
})
export class AlertComponent {

  @Input()
  color = 'alert-primary';

}
