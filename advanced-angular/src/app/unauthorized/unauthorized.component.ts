import { Component } from '@angular/core';
import { AlertComponent } from '../alert/alert.component';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [AlertComponent],
  templateUrl: './unauthorized.component.html'
})
export class UnauthorizedComponent {

}
