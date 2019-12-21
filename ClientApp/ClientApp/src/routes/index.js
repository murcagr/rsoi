import React from 'react'
import { Route, Switch } from 'react-router'
import CatsPage from '../components/CatsPage';
import Counter from '../components/Counter';
import Oauth from '../components/Oauth';

const routes = (
    <div>
        <Switch>
            <Route exact path="/" component={Counter} />
            <Route exact path="/cats" component={CatsPage} />
            <Route exact path="/oacallback" component={Oauth} />
        </Switch>
    </div>
)

export default routes