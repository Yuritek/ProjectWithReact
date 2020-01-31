import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { AddContact } from './components/AddContact'; 
import { Contact } from './components/Contact';


export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={FetchData} />
    <Route path='/fetchemployee' component={Contact} />
    <Route path='/addemployee' component={AddContact} />
    <Route path='/employee/edit/:empid' component={AddContact} />  
</Layout>;
