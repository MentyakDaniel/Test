import { Component, OnInit, ViewChild } from '@angular/core';
import { TestService } from './services/test.service';
import { tap } from 'rxjs';
import { Operator } from './interfaces/operator.interface';
import { DialogBoxComponent } from './dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  @ViewChild(MatTable, { static: true }) table!: MatTable<any>;

  dataSource!: Operator[];
  displayedColumns: string[] = ['name', 'code', 'action'];
  countBun: number | null = null;

  constructor(private testService: TestService, public dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.updateOperators();

    setInterval(() => {
      this.updateOperators();
    }, 3000);
  }


  updateOperators() {
    this.testService.getAllOperators().pipe(
      tap(result => {
        this.dataSource = result;
      })
    ).subscribe();
  }

  openDialog(action: string, obj: Operator | null) {

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: {
        operator: obj,
        action: action
      },

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Add') {
        this.addRowData(result.data);
      } else if (result.event == 'Update') {
        this.updateRowData(result.data);
      } else if (result.event == 'Delete') {
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(row_obj: Operator) {
    this.testService.addOperator(row_obj.name).pipe(
      tap(result => {
        this.updateOperators();
        this.table.renderRows();
      })
    ).subscribe();
  }
  updateRowData(row_obj: Operator) {
    this.testService.updateOperator(row_obj).pipe(
      tap(result => {
        this.updateOperators();
        this.table.renderRows();
      })
    ).subscribe();
  }
  deleteRowData(row_obj: Operator) {
    this.testService.deleteOperator(row_obj.id).pipe(
      tap(result => {
        this.updateOperators();
        this.table.renderRows();
      })
    ).subscribe();
  }
}
