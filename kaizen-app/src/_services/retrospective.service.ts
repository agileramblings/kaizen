import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { RetrospectiveDetails } from '../_models/retrospective';
import { RETROSPECTIVES } from '../app/mock-retrospective';

@Injectable({
  providedIn: 'root',
})
export class RetrospectiveService {

  constructor() { }

  getRetrospectives(): RetrospectiveDetails[] {
    return RETROSPECTIVES;
  }
}
