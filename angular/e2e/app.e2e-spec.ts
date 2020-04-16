import { AzmoonTemplatePage } from './app.po';

describe('Azmoon App', function() {
  let page: AzmoonTemplatePage;

  beforeEach(() => {
    page = new AzmoonTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
