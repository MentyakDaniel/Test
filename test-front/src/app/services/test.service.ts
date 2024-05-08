import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "src/environments/environments";
import { Operator } from "../interfaces/operator.interface";
import { Injectable } from "@angular/core";

@Injectable()
export class TestService {
    constructor(private http: HttpClient) { }

    getAllOperators(): Observable<Operator[]> {
        return this.http.get<Operator[]>(`${environment.apiUrl}/operators`);
    }

    getOperatorByCode(code: number): Observable<Operator> {
        return this.http.get<Operator>(`${environment.apiUrl}/operators/${code}`);
    }

    addOperator(name: string): Observable<Operator> {
        return this.http.post<Operator>(`${environment.apiUrl}/operators/add`, `\"${name}\"`,{
            headers: new HttpHeaders({ 'accept': '*/*', 'Content-Type': 'application/json' })
        });
    }

    updateOperator(operator: Operator): Observable<Operator> {
        return this.http.put<Operator>(`${environment.apiUrl}/operators/update`, operator);
    }

    deleteOperator(guid: string): Observable<Operator> {
        return this.http.delete<Operator>(`${environment.apiUrl}/operators/delete/${guid}`);
    }
}