import React from 'react'
import { Route, Switch } from 'react-router'
import CatsPage from '../components/CatsPage';
import Counter from '../components/Counter';
import Login from '../components/Login';
import SignUp from '../components/SignUp';

const routes = (
    <div>
        <Switch>
            <Route exact path="/" component={Counter} />
            <Route exact path="/cats" component={CatsPage} />
            <Route exact path="/signup" component={SignUp} />
            <Route exact path="/login" component={Login} />
        </Switch>
    </div>
)

export default routes