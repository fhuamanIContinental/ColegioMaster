import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const apiMessage =
        typeof error.error?.textMessage === 'string'
          ? error.error.textMessage
          : typeof error.error?.TextMessage === 'string'
            ? error.error.TextMessage
            : error.message;

      return throwError(() => new Error(apiMessage || 'Ocurrió un error al consumir la API.'));
    })
  );
};
