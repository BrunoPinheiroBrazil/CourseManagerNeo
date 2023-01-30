import React, { Component } from 'react';
import { Input } from 'reactstrap';

export class AtualizarCadastro extends Component {
  
  static displayName = AtualizarCadastro.name;

  constructor(props) {
    super(props);
    
    this.state = { currentSelected: 0 };
    this.updateSelectedState = this.updateSelectedState.bind(this);
  }

  updateSelectedState(selected) {
    this.setState({
        currentSelected: selected
    });
  }

  render() {
    return (
      <div>
        <h1>Atualizar Cadastro</h1>
        <div>
          <Input
          />
        </div>
      </div>
    );
  }
}
