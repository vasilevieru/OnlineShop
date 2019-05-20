export interface PagedResult<T> {
    totalCount: number;
    totalPages: number;
    pageSize: number;
    page: number;
    result: T[];
}
