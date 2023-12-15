import { bootstrapApplication } from '@angular/platform-browser';
import { config } from './app/app.config.server';
import { RootComponent } from './app/components/root/root.component';

const bootstrap = () => bootstrapApplication(RootComponent, config);

export default bootstrap;
