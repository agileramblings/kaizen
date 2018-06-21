import { Injectable } from '@angular/core';
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
