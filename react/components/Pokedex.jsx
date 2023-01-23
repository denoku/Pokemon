import React, { useEffect, useState } from "react";
import debug from 'sabio-debug';
import pokemonService from "../services/pokemonService";
import toastr from "toastr";
import SinglePokemon from './SinglePokemon';
import './pokemon.css'

const _logger = debug.extend('Pokedex');

function Pokedex() {

    const [data, setData] = useState({
        arrayOfPokemon: [],
        pokemonComponents: [],
        pageIndex: 0,
        pageSize: 12,
        totalCount: 0
    });

    useEffect(() => {
        pokemonService
            .getPokemon(data.pageIndex, data.pageSize)
            .then(getPokemonSuccess)
            .catch(getPokemonError)
    }, [data.pageSize, data.pageIndex])

    const getPokemonSuccess = (data) => {
        _logger('getPokemonSuccess', data)
        const pokemonData = data.item.pagedItems;
        setData((prev) => {
            const pd = { ...prev };
            pd.arrayOfPokemon = pokemonData
            pd.pokemonComponents = pokemonData.map(mapPokemon);
            pd.pageIndex = data.item.pageIndex;
            pd.pageSize = data.item.pageSize;
            pd.totalCount = data.item.totalCount

            return pd
        })
    }

    const getPokemonError = (err) => {
        _logger('error', err)
        toastr.error('Pokemon not found')
    }

    const mapPokemon = (aPokemon) => {
        _logger('aPokemon', aPokemon)
        return (
            <SinglePokemon
                pokeData={aPokemon}
                key={aPokemon.id}
            />
        );
    };

    const loadMore = () => {
        setData((prev) => {
            const pd = { ...prev };
            pd.pageSize = 24
            return pd
        })
    }

    return (
        <React.Fragment>

            <div className="pokeContainer ">
                {/* <div className="row">
                        <div className="col">
                            <h1>Pokédex</h1>
                        </div>
                    </div> */}
                <section className="pokedex-results">
                    <ul className="results">{data.pokemonComponents}</ul>
                    {/* <div className="no-results column-12 push-1"></div> */}
                    <div className="content-block content-block-full">
                        <button className="btn load-more" onClick={loadMore}>Load more Pokémon</button>
                    </div>
                </section>
            </div>

        </React.Fragment>
    )

}

export default Pokedex;