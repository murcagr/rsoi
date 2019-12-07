import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'
import * as Counter from './Counter';
import * as WeatherForecasts from './WeatherForecasts';
import * as CatsReducer from './CatsReducer';

const reducers = {
    counter: Counter.reducer,
    cats: CatsReducer.reducer,
    weatherForecasts: WeatherForecasts.reducer
};

const createRootReducer = (history) => combineReducers({
    reducers,
    router: connectRouter(history)
   
})
export default createRootReducer