import axios from "axios";
import * as helper from './serviceHelper';

const endpoint = `https://localhost:50001/api/pokedex`;

const getPokemon = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json'},
    }
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getPokemonById = (id) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json'},
    }
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const addPokemon = (payload) => {
    const config = {
        method: 'POST',
        url: endpoint,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    }
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
}

const pokemonService = {
    getPokemon,
    addPokemon,
    getPokemonById
};

export default pokemonService;