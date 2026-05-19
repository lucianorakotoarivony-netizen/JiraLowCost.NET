import { HttpInterceptorFn } from '@angular/common/http';
import { Auth } from '../Services/auth';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(Auth);
  const token = auth.getToken();
  if(token){
    const authReq = req.clone({
      setHeaders:{
        Authorization : `Bearer ${token}`,
      },
    });
    return next(authReq);
  }
  return next(req);
};
