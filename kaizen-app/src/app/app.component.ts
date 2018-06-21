import { Component } from '@angular/core';
import { RETROSPECTIVE } from './mock-retrospective';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Kaizen';
  subtitle = 'Your Retrospective';
  participant = 'dave.white@gettyimages.com';
  retrospective = RETROSPECTIVE;
}
