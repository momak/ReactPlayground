import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { TableProduct } from './components/TableProduct';


export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/fetchdata' component={FetchData} />
                <Route path='/tableproduct' component={TableProduct} />
            </Layout>
        );
    }
}