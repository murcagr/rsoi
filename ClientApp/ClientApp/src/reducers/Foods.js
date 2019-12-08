    import configureStore from '../store/configureStore.js';

const updateFoodsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddFoodType = 'ADD_CAT';
const initialState = { foods: [] };

export const actionCreators = {
    foodsRequest: () => (dispatch) => {
        fetch('https://localhost:5049/api/gw/foods')
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