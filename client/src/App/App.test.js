import { render, screen } from '@testing-library/react';
import App from './App';

test('renders', () => {
  render(<App />);
  const playersElement = screen.getByText(/Players/i);
  expect(playersElement).toBeInTheDocument();
});
