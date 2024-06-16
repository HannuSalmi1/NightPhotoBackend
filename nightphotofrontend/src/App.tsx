import { useState } from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import NavbarMain from './components/NavbarMain';
import Card from './components/Card';
import Navbar from './components/NavbarMain'
import './App.css'
import FetchStringComponent from './components/FetchStringComponent';


function App(){
  return (
    <>
      <NavbarMain/>
      <Card></Card>
      <FetchStringComponent/>
    </>
  )
}

export default App
