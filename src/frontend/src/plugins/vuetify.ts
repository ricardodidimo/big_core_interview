import { createVuetify, type ThemeDefinition } from 'vuetify'
import { fa } from "vuetify/iconsets/fa";
import "@fortawesome/fontawesome-free/css/all.css";

const lightTheme: ThemeDefinition = {
  dark: false,
  colors: {
    background: '#f8f9fa',
    surface: '#ffffff',
    primary: '#008a91',
    'primary-darken-1': '#006b70',
    secondary: '#03DAC6',
    'secondary-darken-1': '#018786',
    error: '#B00020',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FB8C00',

    'table-bg': '#ffffff',
    'header-bg': '#eaeaea',
    'row-even': '#f5f5f5',
    'row-odd': '#ffffff',
    'border-color': '#d1d1d1',
    'text-color': '#222222',
    'hover-bg': '#d6e4f0',
  },
  variables: {
    'border-opacity': 0.12,
  }
}

const darkTheme: ThemeDefinition = {
    dark: true,
    colors: {
      background: '#242424',
      surface: '#2e2e2e',
      primary: '#00adb5',
      'primary-darken-1': '#008c93',
      secondary: '#03DAC6',
      'secondary-darken-1': '#018786',
      error: '#CF6679',
      info: '#2196F3',
      success: '#4CAF50',
      warning: '#FB8C00',
      
      'table-bg': '#2e2e2e',
      'header-bg': '#3a3a3a',
      'row-even': '#323232',
      'row-odd': '#2b2b2b',
      'border-color': '#404040',
      'text-color': '#e0e0e0',
      'hover-bg': '#383b4b',
    },
    variables: {
      'border-opacity': 0.12,
    }
}

export default createVuetify({
  theme: {
    defaultTheme: 'dark',
    themes: {
      dark: darkTheme,
      light: lightTheme,
    },
  },

  icons: {
    defaultSet: "fa",
    sets: { fa },
  },
})
