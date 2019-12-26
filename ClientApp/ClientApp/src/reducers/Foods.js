import configureStore from '../store/configureStore.js';
import { GATEWAY_ADDR } from '../appconfig';
import { OAUTH_ADDR } from '../appconfig';
const updateFoodsType = 'UPDATE_FOODS';
const decrementCountType = 'DECREMENT_COUNT';
const AddFoodType = 'ADD_FOOD';
const initialState = { foods: [] };

export const actionCreators = {
    foodsRequest: () => (dispatch) => {
        fetch(`${GATEWAY_ADDR}/api/gw/foods`)
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateFoods(data));
                console.log("foods data from action: ", data)
            })
            .catch(console.log)
    },

    updateFoods: (foods) => ({ type: updateFoodsType, foods: foods })

};

export const foodsReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateFoodsType) {
        return { foods: action.foods };
    }

    return state;
};

export default foodsReducer