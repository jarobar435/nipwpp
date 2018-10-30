import { NgApiTreningUsPage } from './app.po';

describe('ng-api-trening-us App', () => {
  let page: NgApiTreningUsPage;

  beforeEach(() => {
    page = new NgApiTreningUsPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
