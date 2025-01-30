import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';  // Import the HttpClient service
import { appConfig } from './app/app.config';  // Your custom appConfig
import { AppComponent } from './app/app.component';  // Your main component

bootstrapApplication(AppComponent, {
  providers: [
    ...appConfig.providers,  // Include any existing providers from your appConfig
    provideHttpClient(),  // Add HttpClient globally
  ],
})
  .catch((err) => console.error(err));
