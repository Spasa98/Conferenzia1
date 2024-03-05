import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription, window, windowWhen } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RegistracijaSlusaocaComponent } from '../registracija-slusaoca/registracija-slusaoca.component';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { slusalac } from 'src/app/slusalac';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit, OnDestroy {
  loginForm!: FormGroup;
  subscriptions: Subscription[] = []
  ref!: DynamicDialogRef;
  slusalac: any;
  
  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
    public dialogService: DialogService,
    private slusalacService: SlusalacService,
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
 
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub=>sub.unsubscribe())
  }

  onSubmit() {
    const userData = this.loginForm.getRawValue();
    this.subscriptions.push(
      this.authService.login(userData).subscribe({
        next: (res: any)=>{
          this.router.navigate(['/home']);
          this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Prijava je uspela!'});
        },
        error: err=>{
          this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške.'});
        }
      })
    )
  }

  onRegistration() {
    this.ref = this.dialogService.open(RegistracijaSlusaocaComponent, {
      header: 'Registracija',
      width: '40%'
    });
    // this.ref.onClose.subscribe(res=> {
    //   this.getSlusalac(this.slusalac.id);
    // });
  }

  // getSlusalac(slusalacID:number):void
  // {
  //   this.slusalacService.getSlusalac2(slusalacID).subscribe((slusalac) => {
  //     this.slusalac = slusalac[0]
  //     console.log(this.slusalac)
  //   });
  // }
}

