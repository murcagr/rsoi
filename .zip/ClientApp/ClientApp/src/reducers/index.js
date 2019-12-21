import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'
import counterReducer from './Counter'
import catsReducer from "./Cats"
import ownersReducer from './Owners';
import foodsReducer from './Foods';
import cotsReducer from './COF';

const rootReducer = (history) => combineReducers({
    count: counterReducer,
    cats: catsReducer,
    owners: ownersReducer,
    foods: foodsReducer,
    cots: cotsReducer,
    router: connectRouter(history)
})

export default rootReducer