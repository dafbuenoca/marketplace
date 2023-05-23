import { Category } from "./category.model";
import { PageModel } from "./page.model";

describe('PageModel', () => {

  let items: Category[];
  let pageModel: PageModel<Category>;

  beforeEach(() => {
    
    items = [
      { id: 2, name: "Service"},
      { id: 3, name: "I'm looking for"},
      { id: 1, name: "Product"},
      { id: 4, name: "Undefined"},
      { id: 5, name: "Test"}
    ]
  });

  it('should initialize with a single page available', () => {
    pageModel = new PageModel<Category>(items, 1, 0);
    expect(pageModel.items).toEqual(items);
    expect(pageModel.nextPageIndex).toBeNull();
    expect(pageModel.pageCount).toBe(1);
    expect(pageModel.pageIndex).toBe(0);
    expect(pageModel.previousPageIndex).toBeNull();
  });

  it('should handle many pages (at least 10) and be on the first page', () => {
    const pageCount = 10;
    pageModel = new PageModel<Category>(items, pageCount, 0);
    console.log(pageModel)
    expect(pageModel.items).toEqual(items);
    expect(pageModel.nextPageIndex).toBe(1);
    expect(pageModel.pageCount).toBe(pageCount);
    expect(pageModel.pageIndex).toBe(0);
    expect(pageModel.previousPageIndex).toBeNull();
    expect(pageModel.getNextPageIndexes()).toEqual([1, 2, 3]);
    expect(pageModel.getPreviousPageIndexes()).toEqual([]);
  });

  it('should handle many pages (at least 10) and be on a middle page', () => {
    const pageCount = 10;
    pageModel = new PageModel<Category>(items, pageCount, 4);
    expect(pageModel.items).toEqual(items);
    expect(pageModel.nextPageIndex).toBe(5);
    expect(pageModel.pageCount).toBe(pageCount);
    expect(pageModel.pageIndex).toBe(4);
    expect(pageModel.previousPageIndex).toBe(3);
    expect(pageModel.getNextPageIndexes()).toEqual([5, 6, 7]);
    expect(pageModel.getPreviousPageIndexes()).toEqual([1, 2, 3]);
  });

  it('should handle many pages (at least 10) and be on the last page', () => {
    const pageCount = 10;
    pageModel = new PageModel<Category>(items, pageCount, pageCount - 1);
    expect(pageModel.items).toEqual(items);
    expect(pageModel.nextPageIndex).toBeNull();
    expect(pageModel.pageCount).toBe(pageCount);
    expect(pageModel.pageIndex).toBe(pageCount - 1);
    expect(pageModel.previousPageIndex).toBe(pageCount - 2);
    expect(pageModel.getNextPageIndexes()).toEqual([]);
    expect(pageModel.getPreviousPageIndexes()).toEqual([6, 7, 8]);
  });

  it('PageModel test', () => {
    expect(true).toBeTruthy();
  });

});
