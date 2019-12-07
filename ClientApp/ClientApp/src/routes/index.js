import React from 'react'
import { Route, Switch } from 'react-router'
import CatsPage from '../components/CatsPage';
import CatsTable from '../components/CatsTable';
import Counter from '../components/Counter';
import CatsListPage from '../components/CatListPage';
import OwnerCatsPage from '../components/OwnerCatsPage';

const routes = (
    <div>
        <Switch>
            <Route exact path="/" component={Counter} />
            <Route exact path="/cats" component={CatsPage} />
            <Route exact path="/catss" component={CatsListPage} />
            <Route exact path="/ownerCats" component={OwnerCatsPage} />
        </Switch>
    </div>
)

export default routes