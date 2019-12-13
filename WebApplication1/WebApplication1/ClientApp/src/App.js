import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { User } from './components/User';
import { Product } from './components/Product';
import { Order } from './components/Order';
import { Login } from './components/Login';
import { Register } from './components/Register';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

    render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/orders' component={Order} />
        <Route path='/users' component={User} />
        <Route path='/products' component={Product} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Register} />
      </Layout>
    );
  }
}
