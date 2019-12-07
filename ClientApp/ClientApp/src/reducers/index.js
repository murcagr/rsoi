import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'
import counterReducer from './Counter'
import catsReducer from "./Cats"
import ownersReducer from './Owners';

const rootReducer = (history) => combineReducers({
    count: counterReducer,
    cats: catsReducer,
    owners: ownersReducer,
    router: connectRouter(history)
})

export default rootReducer