export class PageModel<T> {
  
    items: T[]
    
    nextPageIndex: number | null;
    
    pageCount: number;

    pageIndex: number;

    previousPageIndex: number | null;

    constructor(items: T[], pageCount: number, pageIndex: number){
        
        this.items =  items;
        this.pageCount = pageCount;
        this.pageIndex = pageIndex
        this.nextPageIndex = pageIndex < pageCount - 1 ? pageIndex + 1 : null;
        this.previousPageIndex = pageIndex > 0 ? pageIndex - 1 : null;
      }
    
      getNextPageIndexes(): number[] {
        const nextIndexes: number[] = [];
        for (let i = this.pageIndex + 1; i < this.pageIndex + 4 && i < this.pageCount; i++) {
          nextIndexes.push(i);
        }
        return nextIndexes;
      }
    
      getPreviousPageIndexes(): number[] {
        const previousIndexes: number[] = [];
        for (let i = this.pageIndex - 1; i > this.pageIndex - 4 && i >= 0; i--) {
          previousIndexes.unshift(i);
        }
        return previousIndexes;
      }
}
